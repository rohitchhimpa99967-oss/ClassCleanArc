using Application.Dtos.Users;
using Application.Interfaces.Repositories;
using Domain.Entities.Users;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries;

public class GetUserByIdQuery : IRequest<Result<GetUserDto>>
{
    public int Id { get; set; }

    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);

        if(user == null)
        {
            return Result<GetUserDto>.BadRequest("User not found");
        }

        var result = new GetUserDto
        {
            Id=user.Id,
            Name=user.Name,
            Password=user.Password,
            Email=user.Email,
            IsActive=user.IsActive,
            CreateDate=user.CreateDate
        };

        return Result<GetUserDto>.Success(result, "User...  ");
    }
}