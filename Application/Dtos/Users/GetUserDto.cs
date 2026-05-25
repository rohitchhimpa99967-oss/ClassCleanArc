using Application.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Users;

public class GetUserDto : BaseDto
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
