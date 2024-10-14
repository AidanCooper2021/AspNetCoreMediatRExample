using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
		UpdateAddressRequest = new UpdateAddressRequest();

		// Gets the address book entry by ID
		var entries = _repo.Find(new EntryByIdSpecification(id));
		if(entries.Count > 0)
		{
			// Gets the first entry
			AddressBookEntry x = entries[0];

			// Updates each property of UpdateAddressRequest
			UpdateAddressRequest.Id = x.Id;
			UpdateAddressRequest.Line1 = x.Line1;
			UpdateAddressRequest.Line2 = x.Line2;
			UpdateAddressRequest.City = x.City;
			UpdateAddressRequest.State = x.State;
			UpdateAddressRequest.PostalCode = x.PostalCode;
		}

	}

	public ActionResult OnPost()
	{
		// Use mediator to send a "command" to update the address book entry, redirect to entry list.

		// Checks if the model state is valid from the cshtml form
		if(ModelState.IsValid) 
		{
			// Sends the UpdateAddressRequest to the mediator
			Task x = _mediator.Send(UpdateAddressRequest);

			// Redirects to the entry list
			return RedirectToPage("Index");
		}
		
		// If the model state is not valid, return the page
		return Page();
	}
}