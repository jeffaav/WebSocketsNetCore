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
            config.CreateMap<Topic, NewVM>();

            config.CreateMap<NewVM, Topic>()
                .ForMember(d => d.Id, o => o.MapFrom(s => ObjectId.Parse(s.Id)));
        }
    }
}