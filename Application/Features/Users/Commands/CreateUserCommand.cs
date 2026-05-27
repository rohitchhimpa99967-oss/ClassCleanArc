using Application.Interfaces.Repositories;
using Domain.Entities.Users;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<Result<int>>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };

        await _unitOfWork.Repository<User>().PostAsync(user);
        await _unitOfWork.Save(cancellationToken);

        return Result<int>.Success(user.Id, "User Created successfully");
    }
}