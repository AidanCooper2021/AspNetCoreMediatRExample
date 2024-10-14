using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class UpdateAddressHandler
	: IRequestHandler<UpdateAddressRequest, Unit>
{
	private readonly IRepo<AddressBookEntry> _repo;

	public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
	{
		_repo = repo;
	}

	public async Task<Unit> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
	{	
		// Find the entry by ID
		var entries = _repo.Find(new EntryByIdSpecification(request.Id));
		
		// This check may not be necessary, because an update request would not be sent if the entry does not exist. But it is a good practice to check
		if(entries.Count == 0) {
			throw new Exception("Entry not found");
		}

		// Gets the first (and only) entry 
		AddressBookEntry entry = entries[0];

		// Updates the entry with the request data
		entry.Update(request.Line1, request.Line2, request.City, request.State, request.PostalCode);

		// This is also currently not necessary, because the entry is referenced, and the changes are made to the object in memory
		// But, it is a good practice to update the repository. Also, for future implementations, the repository may require this
		_repo.Update(entry);

		// Returns a Unit, just to indicate that the handle was successful
		return await Task.FromResult(Unit.Value);
	}
}