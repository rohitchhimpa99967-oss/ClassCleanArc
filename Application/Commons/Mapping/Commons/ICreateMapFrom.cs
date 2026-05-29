using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commons.Mapping.Commons;

public interface ICreateMapFrom<T>
{
    void CreateMapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
}
