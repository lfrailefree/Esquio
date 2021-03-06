﻿using System;
using System.Collections.Generic;

namespace Esquio.UI.Api.Infrastructure.Data.Entities
{
    public sealed class ProductEntity
        :IAuditable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<FeatureEntity> Features { get; set; }

        public ICollection<DeploymentEntity> Deployments { get; set; }

        public ProductEntity(string name, string description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;

            Features = new List<FeatureEntity>();
            Deployments = new List<DeploymentEntity>();
        }
    }
}
