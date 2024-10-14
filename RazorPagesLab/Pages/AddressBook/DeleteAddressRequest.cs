using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressRequest
	: IRequest
{
	public Guid Id { get; set; }

	[Required(ErrorMessage = "Address line 1 is required.")]
	[DisplayName("Address Line 1")]
	public string Line1 { get; set; }

	[DisplayName("Address Line 2")]
	public string Line2 { get; set; }

	[Required(ErrorMessage = "City is required.")]
	public string City { get; set; }

	[Required(ErrorMessage = "State is required.")]
	public string State { get; set; }

	[DisplayName("Postal Code")]
	[Required(ErrorMessage = "Postal code is required.")]
	public string PostalCode { get; set; }
}