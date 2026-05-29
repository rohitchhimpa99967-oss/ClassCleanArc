using Application.Commons.Mapping.Commons;
using Application.Dtos.Commons;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Users;

public class GetUserDto : BaseDto,IMapFrom<User>
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
