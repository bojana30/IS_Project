using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IReviewService
    {
        IEnumerable<Review> GetReviewsForBook(Guid bookId);
        Review AddReview(Review review);
        Review DeleteReview(Guid id);
    }
}
