using System.Threading.Tasks;
using Marten.Testing.Documents;
using Microsoft.Extensions.Hosting;

namespace Marten.Testing.Examples;

public interface IInvoicingStore : IDocumentStore
{
}

public class DocumentCleanerExamples
{
    #region sample_clean_out_documents
    public async Task clean_out_documents(IDocumentStore store)
    {
        // Completely remove all the database schema objects related
        // to the User document type
        await store.Advanced.Clean.CompletelyRemoveAsync(typeof(User));

        // Tear down and remove all Marten related database schema objects
        await store.Advanced.Clean.CompletelyRemoveAllAsync();

        // Deletes all the documents stored in a Marten database
        await store.Advanced.Clean.DeleteAllDocumentsAsync();

        // Deletes all the event data stored in a Marten database
        await store.Advanced.Clean.DeleteAllEventDataAsync();

        // Deletes all of the persisted User documents
        await store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(User));

        // For cases where you may want to keep some document types,
        // but eliminate everything else. This is here specifically to support
        // automated testing scenarios where you have some static data that can
        // be safely reused across tests
        await store.Advanced.Clean.DeleteDocumentsExceptAsync(typeof(Company), typeof(User));
    }

    #endregion


    #region sample_clean_out_documents_ihost
    public async Task clean_out_documents(IHost host)
    {
        // Clean off all Marten data in the default DocumentStore for this host
        await host.CleanAllMartenDataAsync();
    }
    #endregion

    #region sample_clean_out_documents_ihost_specific_database
    public async Task clean_out_database_documents(IHost host)
    {
        // Clean off all Marten data in the IInvoicing DocumentStore for this host
        await host.CleanAllMartenDataAsync<IInvoicingStore>();
    }
    #endregion
}
