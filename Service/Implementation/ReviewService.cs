using Domain.DomainModels;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _reviewRepository;

        public ReviewService(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IEnumerable<Review> GetReviewsForBook(Guid bookId)
        {
            return _reviewRepository.GetAll(r => r, r => r.BookId == bookId);
        }

        public Review AddReview(Review review)
        {
            return _reviewRepository.Insert(review);
        }

        public Review DeleteReview(Guid id)
        {
            var review = _reviewRepository.Get(r => r, r => r.Id == id);
            return _reviewRepository.Delete(review);
        }
    }
}
