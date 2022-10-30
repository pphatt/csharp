create table Book
(
    BookIDs             int primary key IDENTITY (1,1),
    BookName            nvarchar(100)                      not null,
    CategoryIDs         int                                not null,
    BookAmountAvailable int,
    BookAmountBorrowed  int,
    Date                datetime,
    PublishDate         nvarchar(50),
    State               int check (State = 0 or State = 1) not null,
    LIDs                int,
)

create table BookAuthor
(
    BookIDs   int not null,
    AuthorIDs int not null,
)

create table Author
(
    IDs   int IDENTITY (1,1)                 not null,
    Name  nvarchar(50)                       not null unique,
    State int check (State = 0 or State = 1) not null,
)

create table Category
(
    IDs   int IDENTITY (1,1) primary key,
    Name  nvarchar(50)                       not null unique,
    State int check (State = 0 or State = 1) not null,
)

create table Customer
(
    CustomerIDs         int primary key IDENTITY (1,1),
    CustomerName        nvarchar(50)                       not null,
    CustomerAge         int check (18 <= CustomerAge and CustomerAge <= 80),
    CustomerSex         nvarchar(10) check (CustomerSex = 'Male' or CustomerSex = 'Female'),
    CustomerPhoneNumber nvarchar(15) check (10 <= LEN(CustomerPhoneNumber) and LEN(CustomerPhoneNumber) <= 15),
    Date                datetime,
    State               int check (State = 0 or State = 1) not null,
    LIDs                int,
)

create table BookLog
(
    BookIDs      int                                not null,
    CustomerIDs  int                                not null,
    DateBorrow   datetime,
    LIDsCheckIn  int,
    DateReturn   datetime,
    LIDsCheckOut int,
    -- DateDue date,
    State        int check (State = 0 or State = 1) not null,
)

create table Librarian
(
    LibrarianIDs         int primary key IDENTITY (1, 1),
    LibrarianName        nvarchar(50)                       not null,
    LibrarianAge         int check (22 <= LibrarianAge and LibrarianAge <= 80),
    LibrarianSex         nvarchar(10) check (LibrarianSex = 'Male' or LibrarianSex = 'Female'),
    LibrarianPhoneNumber nvarchar(15) check (10 <= LEN(LibrarianPhoneNumber) and LEN(LibrarianPhoneNumber) <= 15),
    DateEnrol            datetime,
    DateRetire           datetime,
    State                int check (State = 0 or State = 1) not null,
)

create table Scheduled
(
    DateOfWeek   nvarchar(11),
    TimeStart    time,
    TimeEnd      time,
    LibrarianIDs int,
)

alter table Book
    add constraint FK_B_LIDs foreign key (LIDs) references Librarian (LibrarianIDs)

alter table Customer
    add constraint FK_C_LIDs foreign key (LIDs) references Librarian (LibrarianIDs)

alter table BookLog
    add constraint FK_BLI_LIDs foreign key (LIDsCheckIn) references Librarian (LibrarianIDs)

alter table BookLog
    add constraint FK_BLO_LIDs foreign key (LIDsCheckOut) references Librarian (LibrarianIDs)

alter table BookLog
    add constraint FK_B_BookLog foreign key (BookIDs) references Book (BookIDs)

alter table BookLog
    add constraint FK_C_BookLog foreign key (CustomerIDs) references Customer (CustomerIDs)

alter table BookLog
    add constraint PK_BookLog primary key (CustomerIDs, BookIDs)

alter table Scheduled
    add constraint FK_Scheduled foreign key (LibrarianIDs) references Librarian (LibrarianIDs)

alter table Author
    add constraint PK_Author primary key (IDs)

alter table BookAuthor
    add constraint PK_BookAuthor PRIMARY KEY (BookIDs, AuthorIDs)

alter table BookAuthor
    add constraint FK_BookAuthor_BookIDs foreign key (BookIDs) references Book (BookIDs)

alter table BookAuthor
    add constraint FK_BookAuthor_AuthorIDs foreign key (AuthorIDs) references Author (IDs)

alter table Book
    add constraint FK_Book foreign key (CategoryIDs) references Category (IDs)

-- trigger that check input
create trigger Customer_Insert
    on Customer
    after insert, update as
begin
    if exists(select CustomerPhoneNumber
              from Customer
              where LEN(CustomerPhoneNumber) < 10
                 or LEN(CustomerPhoneNumber) > 15)
        begin
            print 'Invalid Phone Number'
            rollback tran
        end
    else
        if exists(select CustomerSex from Customer where CustomerSex != 'Male' or CustomerSex != 'Female')
            begin
                print 'Invalid Customer Sex'
                rollback tran
            end
end
go

-- trigger that check input
create trigger Librarian_Insert
    on Librarian
    after insert, update as
begin
    if exists(select LibrarianPhoneNumber
              from Customer
              where LEN(LibrarianPhoneNumber) < 10
                 or LEN(LibrarianPhoneNumber) > 15)
        begin
            print 'Invalid Librarian Number'
            rollback tran
        end
    else
        if exists(select LibrarianSex from Customer where LibrarianSex != 'Male' or LibrarianSex != 'Female')
            begin
                print 'Invalid Librarian Sex'
                rollback tran
            end
        else
            if exists(select LibrarianAge from Customer where LibrarianAge < 22 or LibrarianAge > 80)
                begin
                    print 'Invalid Librarian Age'
                    rollback tran
                end
end
go

-- trigger that will add or subtract the amount of book when someone return
create trigger BookLog_Update
    on BookLog
    after update as
begin
    if update(State)
        begin
            update Book
            set BookAmountAvailable += 1,
                BookAmountBorrowed -= 1
            where BookIDs = (select top (1) BookIDs
                             from BookLog
                             where State = 1
                             order by DateReturn desc)
        end
end
go

-- trigger that will add or subtract the amount of book when someone borrow
create trigger BookLog_Insert
    on BookLog
    after insert as
begin
    update Book
    set BookAmountAvailable -= 1,
        BookAmountBorrowed += 1
    where BookIDs = (select top (1) BookIDs
                     from BookLog
                     where State = 0
                     order by DateBorrow desc)
end
go

-- this function will get the author id for add it to the BookAuthor section. It will check if there is author's name in
-- the database yet. if not it will automatically add and pull out the id else it will pull out the id
create function GetAuthorID(@author_name nvarchar(50))
    returns int
as
begin
    declare @id int
    select @id = Author.IDs
    from Author
    where @author_name = Name

    return @id
end
go

create procedure CheckAuthor @author_name nvarchar(50) as
declare @id int
select @id = [dbo].GetAuthorID(@author_name)
    if (@id IS NOT NULL)
        begin
            print @id
        end
    else
        begin
            insert into Author (Name, State) values (@author_name, 0)

            select @id = [dbo].GetAuthorID(@author_name)

            print @id
        end
go

-- this function will get the category id for add it to the Book section. It will check if there is category's name in
-- the database yet. if not it will automatically add and pull out the id else it will pull out the id
create function GetCategoryID(@category_name nvarchar(50))
    returns int
as
begin
    declare @id int
    select @id = Category.IDs
    from Category
    where @category_name = Name

    return @id
end
go

create procedure CheckCategory @category_name nvarchar(50) as
declare @id int
select @id = [dbo].GetCategoryID(@category_name)
    if (@id IS NOT NULL)
        begin
            print @id
        end
    else
        begin
            insert into Category (Name, State) values (@category_name, 0)

            select @id = [dbo].GetCategoryID(@category_name)

            print @id
        end
go

-- this procedure is for when adjust the information of author and it automatically update in every thing that related to it
create procedure EditAuthor @author_name nvarchar(50), @book_id int as
declare @id int
select @id = [dbo].GetAuthorID(@author_name)
    if (@id IS NOT NULL)
        begin
            update BookAuthor set AuthorIDs = @id where BookIDs = @book_id
        end
    else
        begin
            insert into Author (Name, State) values (@author_name, 0)

            select @id = [dbo].GetAuthorID(@author_name)

            update BookAuthor set AuthorIDs = @id where BookIDs = @book_id
        end
go

-- this procedure is for when adjust the information of category and it automatically update in every thing that related to it
create procedure EditCategory @category_name nvarchar(50), @book_id int as
declare @id int
select @id = [dbo].GetCategoryID(@category_name)
    if (@id IS NOT NULL)
        begin
            update Book set CategoryIDs = @id where BookIDs = @book_id
        end
    else
        begin
            insert into Category (Name, State) values (@category_name, 0)

            select @id = [dbo].GetCategoryID(@category_name)

            update Book set CategoryIDs = @id where BookIDs = @book_id
        end
go

-- this procedure is for pulling out the today, at this time who is the librarian that take the shift
create procedure GetLibrarian as
declare @id int
Select @id = Librarian.LibrarianIDs
from (Scheduled left join Librarian on Scheduled.LibrarianIDs = Librarian.LibrarianIDs)
where Scheduled.DateOfWeek = datename(weekday, current_timestamp)
  and CONVERT(TIME, CURRENT_TIMESTAMP) between TimeStart and TimeEnd
    print @id
go

-- The order dropping table
drop table BookLog
drop table BookAuthor
drop table Book
drop table Author
drop table Category
drop table Customer
drop table Scheduled
drop table Librarian

-- Reset the IDENTITY of each primary key
Dbcc checkident (Book, reseed, 0)
Dbcc checkident (Author, reseed, 0)
Dbcc checkident (Customer, reseed, 0)
Dbcc checkident (Category, reseed, 0)
Dbcc checkident (Librarian, reseed, 0)

-- 20 Queries
select top (1) with ties Book.BookIDs,
                         BookName,
                         Author.Name,
                         Category.Name,
                         BookAmountAvailable,
                         BookAmountBorrowed,
                         Book.date,
                         Count(CustomerIDs) as Amount
from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0),
     Category,
     Author,
     BookAuthor
where Book.State = 0
  and Category.IDs = Book.CategoryIDs
  and Author.IDs = BookAuthor.AuthorIDs
  and BookAuthor.BookIDs = Book.BookIDs
group by Book.BookIDs,
         BookName,
         Author.Name,
         Category.Name,
         BookAmountAvailable,
         BookAmountBorrowed,
         Book.date
order by Count(CustomerIDs) desc

select top (1) WITH TIES Customer.CustomerIDs,
                         CustomerName,
                         CustomerAge,
                         CustomerSex,
                         CustomerPhoneNumber,
                         Customer.Date,
                         Count(BookLog.BookIDs) as Amount
from (Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs and BookLog.State = 0)
where Customer.State = 0
  and BookLog.State = 0
group by Customer.CustomerIDs,
         CustomerName,
         CustomerAge,
         CustomerSex,
         CustomerPhoneNumber,
         Customer.Date
order by Count(BookLog.BookIDs) desc

select Librarian.LibrarianIDs,
       LibrarianName,
       LibrarianAge,
       LibrarianSex,
       LibrarianPhoneNumber,
       DateEnrol,
       Count(Scheduled.LibrarianIDs) as Amount
from Librarian,
     Scheduled
where Librarian.State = 0
  and Scheduled.LibrarianIDs = Librarian.LibrarianIDs
group by Librarian.LibrarianIDs, LibrarianName, LibrarianAge, LibrarianSex, LibrarianPhoneNumber, DateEnrol
having Count(Scheduled.LibrarianIDs) in (select distinct top (2) Count(Scheduled.LibrarianIDs)
                                         from Scheduled
                                         group by LibrarianIDs
                                         order by Count(Scheduled.LibrarianIDs) desc)
order by Count(Scheduled.LibrarianIDs) desc

select distinct top (2) Count(Scheduled.LibrarianIDs)
from Scheduled
group by LibrarianIDs
order by Count(Scheduled.LibrarianIDs) desc

-- Nhung quyen sach dc muon nhieu nhat nhieu nhat va` nhieu thu 2
select Book.BookIDs, Book.BookName, Count(BookLog.BookIDs) as Amount
from Book,
     Author,
     BookAuthor,
     Category,
     BookLog
where Book.BookIDs = BookAuthor.BookIDs
  and Author.IDs = BookAuthor.AuthorIDs
  and Category.IDs = Book.CategoryIDs
  and BookLog.BookIDs = Book.BookIDs
group by Book.BookIDs, Book.BookName, Author.Name, Category.Name
having Count(BookLog.BookIDs) in (select distinct top (2) Count(BookLog.BookIDs) as Amount
                                  from BookLog
                                  group by BookLog.BookIDs
                                  order by Count(BookLog.BookIDs) desc)
order by Amount desc

-- cho biet ma khach hang, ten khach hang, ma sach, ten sach da muon. Khach hang nao khong muon thi` ma~ sach, ten sach null
select Customer.CustomerIDs, Customer.CustomerName, BookLog.BookIDs, Book.BookName
from ((Customer left join BookLog on Customer.CustomerIDs = BookLog.CustomerIDs) left join Book
      on Book.BookIDs = BookLog.BookIDs)

-- trigger when update booklog auto update the amount of

-- create trigger Book_Insert
--     on Book
--     after insert, update as
-- begin
--     if exists(select * from inserted where inserted.BookAmountAvailable <= 0 or inserted.BookAmountAvailable > 100)
--         begin
--             print 'Book Amount have to be above 0 and below 100'
--             rollback tran
--         end
--     else
--         if exists(select * from inserted where inserted.BookAmountBorrowed <= 1)
--             begin
--                 print 'There is no book left'
--                 rollback tran
--             end
-- end
-- go

-- insert into Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, Date, State, LIDs) 
-- values (), 
--        ()

-- select LibrarianName, Date, TimeStart, TimeEnd
-- from (Librarian left join Scheduled on Librarian.LibrarianIDs = Scheduled.LibrarianIDs)
-- 
-- select Book.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Book.Date,
--        CustomerIDs
-- from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0)
-- where cast(Book.Date as date) = '2022/10/07'
--   and Book.State = 0
-- 
-- select Book.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Book.Date,
--        CustomerIDs
-- from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0)
-- where Book.Date between '2022-10-01 18:29:12.000' and '2022-10-07 23:59:59.000'
--   and Book.State = 0
-- 
-- select Book.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Book.Date,
--        CustomerIDs
-- from (Book left join BookLog on BookLog.BookIDs = Book.BookIDs and BookLog.State = 0)
-- where datediff(month, Book.Date, '10/9/2022') = 2
--   and Book.State = 0
-- 
-- select LibrarianName, Librarian.LibrarianIDs
-- from (Scheduled left join Librarian on Librarian.LibrarianIDs = Scheduled.LibrarianIDs)
-- where '15:00' between TimeStart and TimeEnd
--   and DateOfWeek = 'Monday'

-- select Customer.CustomerIDs,
--        CustomerName,
--        CustomerAge,
--        CustomerSex,
--        CustomerPhoneNumber,
--        Customer.Date,
--        BookAmount.BookIDs
-- from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs and BookAmount.State = 0)
-- where Customer.State = 0
--   and Customer.CustomerIDs = 8
-- 
-- select Customer.CustomerIDs,
--        CustomerName,
--        CustomerAge,
--        CustomerSex,
--        CustomerPhoneNumber,
--        Customer.Date,
--        BookAmount.BookIDs
-- from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs and BookAmount.State = 0)
-- where Customer.State = 0
-- 
-- Select BookAmount.BookIDs, CustomerIDs, BookAmountAvailable, BookAmountBorrowed
-- from (BookAmount left join Books on Books.BookIDs = BookAmount.BookIDs)
-- where BookAmount.BookIDs = 1
--   and CustomerIDs = 9

-- select (select distinct DateOfWeek from Scheduled)
-- from Scheduled

-- select DateOfWeek, TimeStart, TimeEnd
-- from (Scheduled left join Librarian on Librarian.LibrarianIDs = Scheduled.LibrarianIDs)
-- where DateOfWeek in ('Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday')
--   and Scheduled.LibrarianIDs IS NULL

-- select Customer.CustomerIDs,
--        CustomerName,
--        CustomerAge,
--        CustomerSex,
--        CustomerPhoneNumber,
--        Customer.Date,
--        Customer.State,
--        BookAmount.BookIDs
-- from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs)
-- where Customer.State = 0
--   and CustomerName in 'P%'

-- select Books.BookIDs,
--        Books.BookName,
--        Books.BookAuthor,
--        Books.BookCategory,
--        Books.BookAmountAvailable,
--        Books.BookAmountBorrowed,
--        Books.Date,
--        Books.State,
--        BookAmount.BookIDs,
--        BookAmount.CustomerIDs,
--        BookAmount.Date,
--        BookAmount.State
-- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- where Books.State = 0
--   and BookAmount.State = 0

-- INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
-- VALUES ('John', 20, 'Male', '0900900900', '')
-- 
-- INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
-- VALUES ('Math', 20, 'Male', '0900900900', '')
-- 
-- INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
-- VALUES ('Betty', 20, 'Female', '0900900900', '')
-- 
-- INSERT INTO Customer (CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus)
-- VALUES ('Sin', 20, 'Male', '0900900900', '')

-- select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus
-- from Customer,
--      BookAmount
-- where Customer.CustomerIDs = BookAmount.CustomerIDs
-- group by Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, CustomerStatus
-- 
-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        BookAmount.CustomerIDs,
--        BookAmount.Date
-- from Books,
--      BookAmount
-- where Books.BookIDs = BookAmount.BookIDs
-- order by Books.BookIDs, CustomerIDs, BookAmount.Date
-- 
-- select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, BookAmount.BookIDs
-- from Customer,
--      BookAmount
-- where Customer.CustomerIDs = BookAmount.CustomerIDs
-- order by Customer.CustomerIDs, BookAmount.BookIDs

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

-- select IDs,
--        Name,
--        Author,
--        Cate,
--        Available,
--        Borrowed,
--        Dat,
--        bool1
-- from (select Books.BookIDs                                                                             as IDs,
--              BookName                                                                                  as Name,
--              BookAuthor                                                                                as Author,
--              BookCategory                                                                              as Cate,
--              BookAmountAvailable                                                                       as Available,
--              BookAmountBorrowed                                                                        as Borrowed,
--              Books.Date                                                                                as Dat,
--              IIF(exists(select * where Books.BookIDs = BookAmount.BookIDs), BookAmount.CustomerIDs, 0) as bool1
-- 
--       from Books,
--            BookAmount) as a
-- 
-- group by IDs, Name, Author, Cate, Available, Borrowed, Dat, bool1
-- -- where Books.BookIDs = BookAmount.BookIDs and Books.BookIDs = 1
-- -- order by IDs
-- 
-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        BookAmount.CustomerIDs,
--        BookAmount.Date
-- from Books,
--      BookAmount
-- where Books.BookIDs = BookAmount.BookIDs
-- order by Books.BookIDs, CustomerIDs, BookAmount.Date

-- delete from BookAmount where BookAmount.BookIDs = 2 and BookAmount.CustomerIDs = 0

-- select BookIDs, CustomerIDs, Date
-- from BookAmount

-- Select BookIDs, BookName, BookAuthor, BookCategory, BookAmount, Date, CustomerIDs from Books where BookName like '1%'

-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        CustomerIDs
-- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- where Books.BookIDs = 1
-- 
-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        CustomerIDs
-- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- where BookAmountAvailable > 0
-- 
-- select Books.BookIDs,
--        BookName,
--        BookAuthor,
--        BookCategory,
--        BookAmountAvailable,
--        BookAmountBorrowed,
--        Books.Date,
--        CustomerIDs
-- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- where Books.Date between '09/30/2022 00:00:00' and '10/03/2022 23:59:59'
-- 
-- delete
-- from Customer
-- where CustomerName = 'Betty'
-- 
-- select BookName, case when exists(select * where Books.BookIDs = 2) then 1 else 0 end as Bool
-- from Books
-- -- where BookIDs = 2
-- -- where Books.Date between '2022/10/03 00:00:00' and '2022/10/03 23:59:59.999'
-- 
-- select Customer.CustomerIDs, CustomerName, CustomerAge, CustomerSex, CustomerPhoneNumber, BookIDs
-- from (Customer left join BookAmount on Customer.CustomerIDs = BookAmount.CustomerIDs)
-- where Customer.CustomerIDs = 1
-- 
-- -- select Books.BookIDs,
-- --        BookName,
-- --        BookAuthor,
-- --        BookCategory,
-- --        BookAmountAvailable,
-- --        BookAmountBorrowed,
-- --        Books.Date,
-- --        CustomerIDs
-- -- from (Books left join BookAmount on BookAmount.BookIDs = Books.BookIDs)
-- -- where cast(Books.Date as date) = '2022/06/10'
-- -- where Books.Date between '2022/06/10 00:00' and '2022/06/10 23:59:59'
-- --   and Books.State = 0
-- 
-- update Books
-- set BookAmountAvailable = 19,
--     BookAmountBorrowed  = 1
-- where BookIDs = 3

-- select Count(BookIDs)
-- from Books
-- where Books.State = 0
-- 
-- -- declare @max int
-- -- select @max = max(BookIDs) from Books
-- -- if @max IS NULL   --check when max is returned as null
-- --     SET @max = 0
-- -- 