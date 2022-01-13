using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Interfaces
{
	public interface IAssignmentService
	{
		PagedResponseModel<AssignmentDto> GetByPage(AssignmentQueryCriteriaDto assignmentQueryCriteria, CancellationToken cancellationToken);
	}
}
