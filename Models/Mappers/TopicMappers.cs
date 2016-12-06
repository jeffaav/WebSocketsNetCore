using AutoMapper;
using MongoDB.Bson;
using WebSocketsNetCore.Models.Entities;
using WebSocketsNetCore.ViewModels.Topic;

namespace WebSocketsNetCore.Models.Mappers
{
    public class TopicMappers 
    {
        public static void RegisterMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Topic, NewVM>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s._id.ToString()))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.name));

            config.CreateMap<NewVM, Topic>()
                .ForMember(d => d._id, o => o.MapFrom(s => ObjectId.Parse(s.Id)))
                .ForMember(d => d.name, o => o.MapFrom(s => s.Name));
        }
    }
}