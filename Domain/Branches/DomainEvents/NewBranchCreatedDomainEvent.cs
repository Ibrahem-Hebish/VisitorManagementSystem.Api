namespace Domain.Branches.DomainEvents;

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
