using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Domain.Exceptions;

namespace Skittles.WebApi.Application.Pubs.Create.v1;

public sealed class CreatePubHandler(
    ILogger<CreatePubHandler> logger,
    [FromKeyedServices("skittles:pubs")] IRepository<Pub> repository)
        : IRequestHandler<CreatePubRequest, CreatePubResponse>
{
    public async Task<CreatePubResponse> Handle(CreatePubRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new PlayerValidationException(nameof(request.Name), "Pub name is required");
        }

        if (string.IsNullOrWhiteSpace(request.Postcode))
        {
            throw new PlayerValidationException(nameof(request.Postcode), "Pub postcode is required");
        }

        // Check if name already exists
        var existingPub = await repository.FirstOrDefaultAsync(
            new PubNameSpec(request.Name), cancellationToken);

        if (existingPub is not null)
        {
            throw new DuplicatePubNameException(request.Name);
        }

        var pub = Pub.Create(request.Name, request.Postcode, request.IsDeleted);

        await repository.AddAsync(pub, cancellationToken);
        logger.LogInformation("pub created {PubId}", pub.Id);

        return new CreatePubResponse(pub.Id);
    }

    internal class PubNameSpec : Specification<Pub>
    {
        public PubNameSpec(string name)
        {
            Query
                .Where(p => p.Name.ToLower() == name.ToLower());
        }
    }
}