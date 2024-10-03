﻿using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications.Specification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Specification.Classes
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criterial { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpecification()
        {
        }
        public BaseSpecification(Expression<Func<T, bool>> CriterialExpression)
        {
            Criterial = CriterialExpression;
        }
    }
}