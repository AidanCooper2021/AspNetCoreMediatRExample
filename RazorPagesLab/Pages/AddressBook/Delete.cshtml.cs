using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public DeleteAddressRequest DeleteAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
		DeleteAddressRequest = new DeleteAddressRequest();

		// Gets the address book entry by ID
		var entries = _repo.Find(new EntryByIdSpecification(id));
		if(entries.Count > 0)
		{
			// Gets the first entry
			AddressBookEntry x = entries[0];

			// Updates each property of DeleteAddressRequest
			DeleteAddressRequest.Id = x.Id;
			DeleteAddressRequest.Line1 = x.Line1;
			DeleteAddressRequest.Line2 = x.Line2;
			DeleteAddressRequest.City = x.City;
			DeleteAddressRequest.State = x.State;
			DeleteAddressRequest.PostalCode = x.PostalCode;
		}

	}

	public ActionResult OnPost()
	{
		// Use mediator to send a "command" to delete the address book entry, redirect to entry list.

		// Checks if the model state is valid from the cshtml form
		if(ModelState.IsValid) 
		{
			// Sends the DeleteAddressRequest to the mediator
			Task x = _mediator.Send(DeleteAddressRequest);
		}         
		
		// Redirects to the entry list
		return RedirectToPage("Index");
	}
}