create table Books
(
    BookIDs             int primary key not null,
    BookName            nvarchar(50),
    BookAuthor          nvarchar(50),
    BookCategory        nvarchar(50),
    BookAmountAvailable int,
    BookAmountBorrowed  int,
    Date                datetime,
)

create table Customer
(
    CustomerIDs         int primary key not null,
    CustomerName        nvarchar(50),
    CustomerAge         int,
    CustomerSex         nvarchar(10),
    CustomerPhoneNumber nvarchar(12),
    CustomerStatus      nvarchar(100),
)

create table BookAmount
(
    BookIDs     int not null,
    CustomerIDs int not null,
    Date        datetime,
)

alter table Books
    add constraint FK_CustomerIDs foreign key (CustomerIDs) references Customer (CustomerIDs)

alter table BookAmount
    add constraint PK_BookAmount primary key (CustomerIDs, BookIDs)

select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus
from Customer,
     BookAmount
where Customer.CustomerIDs = BookAmount.CustomerIDs
group by Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus

select Books.BookIDs,
       BookName,
       BookAuthor,
       BookCategory,
       BookAmountAvailable,
       BookAmountBorrowed,
       Books.Date,
       BookAmount.CustomerIDs,
       BookAmount.Date
from Books,
     BookAmount
where Books.BookIDs = BookAmount.BookIDs
order by Books.BookIDs, CustomerIDs, BookAmount.Date

select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, BookAmount.BookIDs
from Customer,
     BookAmount
where Customer.CustomerIDs = BookAmount.CustomerIDs
order by Customer.CustomerIDs, BookAmount.BookIDs

-- select Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date,
--        case when exists(select * where Books.BookIDs = BookAmount.BookIDs) 
--            then BookAmount.CustomerIDs 
--            else 0 
--            end as bool1
-- 
-- from Books, BookAmount
-- -- group by Books.BookIDs, BookName, BookAuthor, BookCategory, BookAmountAvailable, BookAmountBorrowed, Books.Date, BookAmount.BookIDs, BookAmount.CustomerIDs,
-- -- where Books.BookIDs = BookAmount.BookIDs and Books.BookIDs = 1
-- order by Books.BookIDs

select IDs, Name, Author, Cate, Available, Borrowed, Dat, bool1
from (
         select Books.BookIDs as IDs, BookName as Name, BookAuthor as Author, BookCategory as Cate, BookAmountAvailable as Available, BookAmountBorrowed as Borrowed, Books.Date as Dat,
                IIF(exists(select * where Books.BookIDs = BookAmount.BookIDs), BookAmount.CustomerIDs, 0) as bool1

         from Books, BookAmount
     ) as a

group by IDs, Name, Author, Cate, Available, Borrowed, Dat, bool1
-- where Books.BookIDs = BookAmount.BookIDs and Books.BookIDs = 1
-- order by IDs

select Books.BookIDs,
       BookName,
       BookAuthor,
       BookCategory,
       BookAmountAvailable,
       BookAmountBorrowed,
       Books.Date,
       BookAmount.CustomerIDs,
       BookAmount.Date
from Books,
     BookAmount
where Books.BookIDs = BookAmount.BookIDs
order by Books.BookIDs, CustomerIDs, BookAmount.Date

-- delete from BookAmount where BookAmount.BookIDs = 2 and BookAmount.CustomerIDs = 0

-- select BookIDs, CustomerIDs, Date
-- from BookAmount


-- Select BookIDs, BookName, BookAuthor, BookCategory, BookAmount, Date, CustomerIDs from Books where BookName like '1%'