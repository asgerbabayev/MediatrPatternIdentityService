using AutoMapper;
using Code.Application.Categories.Commands.CreateCategory;
using Code.Application.Common.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Code.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest;
