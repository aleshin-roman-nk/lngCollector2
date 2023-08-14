﻿using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
    public interface IThExpressionRepo: IRepository<ThExpression>, IPropertyUpdater
    {
        IEnumerable<ThExpression> GetByThoughtId(int thoughtId);
        bool HasWork(int id);
    }
}