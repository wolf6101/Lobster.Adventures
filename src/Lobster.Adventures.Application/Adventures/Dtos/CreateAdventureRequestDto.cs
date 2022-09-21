using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Lobster.Adventures.Application.SeedWork;

namespace Lobster.Adventures.Application.Adventures.Dtos
{
    public class CreateAdventureRequestDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AdventureNodeDto> Nodes { get; set; }
    }
}