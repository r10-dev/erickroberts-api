drop table er.author;

CREATE TABLE er.author (
	authorid serial NOT NULL,
	authorname varchar(255) NOT NULL,
	authorimage text NOT NULL,
	userid int4 NOT NULL,
	CONSTRAINT author_pkey PRIMARY KEY (authorid)
)

