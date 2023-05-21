using System;
using System.Linq.Expressions;
using Application.AssignmentTasks.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IExpressionBuilderService<T>
    {
        public Expression<Func<AssignmentTask, bool>> GetFilter(T filters);
    }
}
