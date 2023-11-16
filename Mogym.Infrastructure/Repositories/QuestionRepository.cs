﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mogym.Domain.Entities;
using Mogym.Infrastructure.Interfaces;

namespace Mogym.Infrastructure.Repositories
{
    public class QuestionRepository:Repository<Question>,IQuestionRepository
    {
        public QuestionRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}