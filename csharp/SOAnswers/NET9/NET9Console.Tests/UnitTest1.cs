using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.TestHelper;
using Moq;

namespace NET9Console.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
public class Customer 
{
    public string Name { get; set; }
    public Address Address { get; set; }
}
public class Address 
{
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Town { get; set; }
    public string Country { get; set; }
    public string Postcode { get; set; }
}
public class AddressValidator : AbstractValidator<Address> 
{
    public AddressValidator()
    {
        RuleFor(address => address.Postcode).NotNull();
        //etc
    }
}
public class CustomerValidator : AbstractValidator<Customer> 
{
    public CustomerValidator(IValidator<Address> addressValidator)
    {
        RuleFor(customer => customer.Name).NotNull();
        RuleFor(customer => customer.Address).SetValidator(addressValidator);
    }
}
public class AddressValidatorTests
{
    private readonly AddressValidator _validator;

    public AddressValidatorTests()
    {
        _validator = new AddressValidator();
    }

    [Test]
    public void Validate_Test()
    {
        var address = new Address
        {
            Line1 = "Line number 1",
            Line2 = "Line number 2",
            Town = "Small Town",
            Country = "Some Countery",
            Postcode = "658542"
        };

        var result = _validator.TestValidate(address);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
public class CustomerValidatorTests
{
    private readonly CustomerValidator _validator;

public CustomerValidatorTests()
{
    var addressValidatorMock = new Mock<IValidator<Address>>();
    addressValidatorMock.Setup(m => m.Validate(It.IsAny<ValidationContext<Address>>()))
        .Returns(new FluentValidation.Results.ValidationResult());

    _validator = new CustomerValidator(addressValidatorMock.Object);
}

    [Test]
    public void Validate_Test()
    {
        var customer = new Customer
        {
            Name = "John Doe",
            Address = new Address()
        };

        var result = _validator.TestValidate(customer);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
public class MySetOfScenarios
{
    private int I;
    private int J;

    [OneTimeSetUp]
    public void BaseSetUp()
    {
        J = 7;
    }
    
    [Test, Order(1)]
    public void TestA()
    {
        I = 42;
        Thread.Sleep(1000);
        TestContext.Out.WriteLine(J);
        J = 1;
    }

    [Test, Order(2)]
    public void TestB()
    {
        TestContext.Out.WriteLine(I);
        TestContext.Out.WriteLine(J);
    }
}
// public class MyClass
// {
//     public readonly List<int> Value { get; } = [];  // Error: Property cannot be 'readonly'
// }
//
// public record MyRecord
// {
//     public readonly string Name { get; init; }  // Error: Property cannot be 'readonly'
// }

public struct MyStruct
{
    public readonly string Name { get; init; }  // OK
}

