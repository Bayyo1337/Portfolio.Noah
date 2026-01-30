using Portfolio.Noah.Models.Blogs;

namespace Portfolio.Noah.Providers.Interfaces;

public interface IBlogProvider
{
   public ValueTask<BlogPage?> GetPage(string relativeUrl, CancellationToken ct = default);

   public ValueTask<List<BlogPage>> GetPages(GetPagesFilter filter, CancellationToken ct = default);

   public ValueTask<List<string>> GetTags(CancellationToken ct = default);
   
   public Task Reload(CancellationToken ct = default);
}