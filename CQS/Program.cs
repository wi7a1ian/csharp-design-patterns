using Autofac;
using CQS.Business.Commands;
using CQS.Business.Queries;
using CQS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQS
{
    /// <summary>
    /// Copy from https://github.com/timsommer/cqs-dotnetcurry-sample
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).AsClosedTypesOf(typeof(IQueryHandler<,>));
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).AsClosedTypesOf(typeof(ICommandHandler<,>));
            var container = builder.Build();

            var commandDispatcher = container.Resolve<ICommandDispatcher>();
            var queryDispatcher = container.Resolve<IQueryDispatcher>();

            var response = queryDispatcher.Dispatch<GetBooksQuery, GetBooksQueryResult>(new GetBooksQuery());

            foreach (var book in response.Books)
            {
                Console.WriteLine("Title: {0}, Authors: {1}, InMyPossession: {2}", book.Title, book.Authors, book.InMyPossession);
            }

            //edit first book
            var _bookToEdit = response.Books.First();
            _bookToEdit.InMyPossession = !_bookToEdit.InMyPossession;
            commandDispatcher.Dispatch<SaveBookCommand, SaveBookCommandResult>(new SaveBookCommand()
            {
                Book = _bookToEdit
            });


            //add new book
            commandDispatcher.Dispatch<SaveBookCommand, SaveBookCommandResult>(new SaveBookCommand()
            {
                Book = new Book()
                {
                    Title = "C# in Depth",
                    Authors = "Jon Skeet",
                    InMyPossession = false,
                    DatePublished = new DateTime(2013, 07, 01)
                }
            });


            response = queryDispatcher.Dispatch<GetBooksQuery, GetBooksQueryResult>(new GetBooksQuery());

            foreach (var _book in response.Books)
            {
                Console.WriteLine("Title: {0}, Authors: {1}, InMyPossession: {2}", _book.Title, _book.Authors, _book.InMyPossession);
            }
        }
    }

    namespace Business.Commands
    {
        using CQS.Data;
        using CQS.Domain;
        using CQS.DataAccess;

        public class SaveBookCommand : Command
        {
            public Book Book { get; set; }
        }

        public class SaveBookCommandResult : IResult
        {
        }

        public class SaveBookCommandHandler : CommandHandler<SaveBookCommand, SaveBookCommandResult>
        {
            public SaveBookCommandHandler(ApplicationDbContext context) : base(context)
            {
            }

            protected override SaveBookCommandResult DoHandle(SaveBookCommand request)
            {
                var _response = new SaveBookCommandResult();

                //attach the book
                //ApplicationDbContext.Books.Attach(request.Book);

                //add or update the book entity
                //ApplicationDbContext.Entry(request.Book).State = request.Book.Id == Constants.NewId ? EntityState.Added : EntityState.Modified;

                //persist changes to the datastore
                //ApplicationDbContext.SaveChanges();

                return _response;
            }

            protected override async Task<SaveBookCommandResult> DoHandleAsync(SaveBookCommand request)
            {
                var _response = new SaveBookCommandResult();

                //attach the book
                //ApplicationDbContext.Books.Attach(request.Book);

                //add or update the book entity
                //ApplicationDbContext.Entry(request.Book).State = request.Book.Id == Constants.NewId ? EntityState.Added : EntityState.Modified;

                //persist changes to the datastore
                //await ApplicationDbContext.SaveChangesAsync();

                return _response;
            }
        }
    }

    namespace Business.Queries
    {
        using CQS.Data;
        using CQS.DataAccess;
        using Domain;
        using System.Linq;

        public class GetBooksQuery : Query
        {
            public bool ShowOnlyInPossession { get; set; }

            //other filters here
        }

        public class GetBooksQueryResult : IResult
        {
            public IEnumerable<Book> Books { get; set; }
        }

        public class GetBooksQueryHandler : QueryHandler<GetBooksQuery, GetBooksQueryResult>
        {
            public GetBooksQueryHandler(ApplicationDbContext applicationDbContext)
                : base(applicationDbContext)
            {
            }

            protected override GetBooksQueryResult Handle(GetBooksQuery request)
            {
                var _result = new GetBooksQueryResult();

                var _bookQuery = ApplicationDbContext.Books.AsQueryable();

                if (request.ShowOnlyInPossession)
                {
                    _bookQuery = _bookQuery.Where(c => c.InMyPossession);
                }

                _result.Books = _bookQuery.ToList();

                return _result;
            }

            protected override async Task<GetBooksQueryResult> HandleAsync(GetBooksQuery request)
            {
                var _result = new GetBooksQueryResult();

                var _bookQuery = await Task.Run(() => ApplicationDbContext.Books.ToList()); //ApplicationDbContext.Books.ToListAsync();

                if (request.ShowOnlyInPossession)
                {
                    _bookQuery = _bookQuery.Where(c => c.InMyPossession).ToList();
                }

                _result.Books = _bookQuery.ToList();

                return _result;
            }
        }
    }
}
