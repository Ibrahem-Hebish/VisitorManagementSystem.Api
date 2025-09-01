using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Common;

namespace Domain.TenantDomain.Branches.DomainEvents;

public record NewBranchCreatedDomainEvent(
                                            Guid TenantId,
                                            Guid BranchId,
                                            string BranchName,
                                            BranchAddress BranchAddress,
                                            string PhoneNumber,
                                            string Email
                                            ) 
    
    : DomainEvent
{ }
