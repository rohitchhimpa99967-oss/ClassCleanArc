using Application.Commons.Mapping.Commons;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Users;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<Result<int>>,ICreateMapFrom<User>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //var user = new User
        //{
        //    Name = request.Name,
        //    Email = request.Email,
        //    Password = request.Password
        //};

        var user = _mapper.Map<User>(request);

        await _unitOfWork.Repository<User>().PostAsync(user);
        await _unitOfWork.Save(cancellationToken);

        return Result<int>.Success(user.Id, "User Created successfully");
    }
}