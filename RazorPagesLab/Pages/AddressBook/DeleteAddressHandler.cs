using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressHandler
	: IRequestHandler<DeleteAddressRequest, Unit>
{
	private readonly IRepo<AddressBookEntry> _repo;

	public DeleteAddressHandler(IRepo<AddressBookEntry> repo)
	{
		_repo = repo;
	}

	public async Task<Unit> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
	{	
		// Find the entry by ID
		var entries = _repo.Find(new EntryByIdSpecification(request.Id));
		
		// This check may not be necessary, because a delete request would not be sent if the entry does not exist. But it is a good practice to check
		if(entries.Count == 0) {
			throw new Exception("Entry not found");
		}

		// Removes the first (and only) entry found from the repository
		_repo.Remove(entries[0]);

		// Returns a Unit, just to indicate that the handle was successful
		return await Task.FromResult(Unit.Value);
	}
}