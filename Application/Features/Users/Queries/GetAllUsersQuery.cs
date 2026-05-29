using Application.Dtos.Users;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Users;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries;

public class GetAllUsersQuery:IRequest<Result<List<GetUserDto>>>
{}

internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<GetUserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetUserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<User>().GetAll();

        //List<GetUserDto> result = users.Select(x => new GetUserDto
        //{
        //    Id = x.Id,
        //    Name = x.Name,
        //    Email = x.Email,
        //    Password = x.Password,
        //    IsActive = x.IsActive,
        //    CreateDate = x.CreateDate
        //}).ToList();

        var result = _mapper.Map<List<GetUserDto>>(users);

        return Result<List<GetUserDto>>.Success(result,"Users... ");
    }
}