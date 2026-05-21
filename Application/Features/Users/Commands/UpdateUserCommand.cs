using Application.Interfaces.Repositories;
using Domain.Entities.Users;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands;

public  class UpdateUserCommand : IRequest<Result<string>>
{
    public UpdateUserCommand(int id, CreateUserCommand createUser)
    {
        Id = id;
        CreateUser = createUser;
    }

    public int Id { get; set; }
    public CreateUserCommand CreateUser { get; set; }
}

internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);

        user.Name=request.CreateUser.Name;
        user.Password=request.CreateUser.Password;
        user.Email=request.CreateUser.Email;

        await _unitOfWork.Repository<User>().PutAsync(request.Id, user);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("User Updated successfully");
    }
}