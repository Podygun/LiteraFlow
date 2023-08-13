CREATE TABLE IF NOT EXISTS PriceOffers
(
    PriceOfferId serial primary key,
	NewPrice decimal(5,2) NOT NULL,
	DateEnda timestamp,
	BookId int references Books (BookId) on update cascade on delete cascade
)