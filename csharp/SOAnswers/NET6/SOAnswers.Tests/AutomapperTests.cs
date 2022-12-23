using AutoMapper;

namespace SOAnswers.Tests;

public class AutomapperTests
{
    [Test]
    public async Task Test1()
    {
        var mapperConfiguration = new MapperConfiguration(e =>
        {
            e.CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            e.CreateMap<ModifyRestaurantDto, Restaurant>()
                .ForAllMembers(opts => opts.Condition((_, _, sourceM) => sourceM is not null));
        });
        var mapper = mapperConfiguration.CreateMapper();
        var destination = new Restaurant{HasDelivery = true};
        // var dictionary = mapper.Map<Dictionary<string, object>>(new ModifyRestaurantDto());
        var map = mapper.Map(new ModifyRestaurantDto(), destination);
    }

    public class ModifyRestaurantDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
        public bool? HasDelivery { get; set; }
    }
    
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }

    }
}