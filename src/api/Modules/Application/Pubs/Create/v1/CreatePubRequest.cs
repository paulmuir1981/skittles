using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Skittles.WebApi.Application.Pubs.Create.v1;

public sealed record CreatePubRequest(
    [Required(ErrorMessage = "Pub name is required")]
    string Name,
    [Required(ErrorMessage = "Pub postcode is required")]
    string Postcode,
    bool IsDeleted = false) : IRequest<CreatePubResponse>;
