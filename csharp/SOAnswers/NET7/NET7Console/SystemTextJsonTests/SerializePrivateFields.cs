using System.Runtime.CompilerServices;

namespace NET7Console.SystemTextJsonTests;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

public class SerializePrivateFields
{
    public static void Do()
    {
        var human = Human.Create("Name", 1);
        human.NotPrivate = 42;
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { AddJsonCtorPubOrPriv, AddPrivateFieldsModifier }
            }
        };
        var serialize = JsonSerializer.Serialize(human, jsonSerializerOptions);
        var deserialize = JsonSerializer.Deserialize<Human>(serialize, jsonSerializerOptions);
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class JsonIncludePrivateFieldsAttribute : Attribute { }
    
    [JsonIncludePrivateFields]
    public class Human
    {
        private string _name;
        private int _age;

        // [JsonConstructor]
        public Human()
        {
            // This constructor should be used only by deserializers.
            _name = null!;
            _age = 0;
        }
        [JsonConstructor]
        private Human(string name, int age)
        {
            // This constructor should be used only by deserializers.
            _name = name;
            _age = age;
        }

        public static Human Create(string name, int age)
        {
            Human h = new()
            {
                _name = name,
                _age = age
            };

            return h;
        }

        [JsonIgnore]
        public string Name
        {
            get => _name;
            set => throw new NotSupportedException();
        }

        [JsonIgnore]
        public int Age
        {
            get => _age;
            set => throw new NotSupportedException();
        }

        public int NotPrivate { get; set; }
    }

    private static void AddJsonCtorPubOrPriv(JsonTypeInfo jsonTypeInfo)
    {
        var constructorInfos =
            jsonTypeInfo.Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var constructorInfo = constructorInfos
            .FirstOrDefault(info => info.IsDefined(typeof(JsonConstructorAttribute)));
        if (constructorInfo is not null)
        {
            jsonTypeInfo.CreateObject = () => constructorInfo.Invoke( constructorInfo
                .GetParameters()
                .Select(t => t.ParameterType.IsValueType ? Activator.CreateInstance(t.ParameterType) : null)
                .ToArray());
        }
    }
    private static void AddPrivateFieldsModifier(JsonTypeInfo jsonTypeInfo)
    {
        if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
            return;

        if (!jsonTypeInfo.Type.IsDefined(typeof(JsonIncludePrivateFieldsAttribute), inherit: false))
            return;

        foreach (FieldInfo field in jsonTypeInfo.Type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            if (field.IsDefined(typeof(CompilerGeneratedAttribute)))
            {
                continue;
            }
            JsonPropertyInfo jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(field.FieldType, field.Name);
            jsonPropertyInfo.Get = field.GetValue;
            jsonPropertyInfo.Set = field.SetValue;

            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
    }

}