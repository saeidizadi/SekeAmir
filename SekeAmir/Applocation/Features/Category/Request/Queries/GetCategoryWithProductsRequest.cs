using Domain;
using MediatR;
using PersianAssistant.Models;

namespace Application.Features.Category.Request.Queries;

public class GetCategoryWithProductsRequest : IRequest<ServiceMessage>
{
    public InputType inputType{ get; set; }
}