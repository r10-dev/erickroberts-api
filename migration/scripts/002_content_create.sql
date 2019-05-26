drop table er.content;

CREAte TABLE er.content (
	contentid serial NOT NULL,
	slug varchar(256) NOT NULL UNIQUE,
	authorid int not null,
	title varchar(256) not null,
	headerimage varchar(256) not null,
	tabimage varchar(256) not null,
	views int,
	stars float,
	body text not null,
	published boolean,
	staged boolean,
	draft boolean,
	created_on timestamp,
	published_on timestamp,
	CONSTRAINT content_pkey PRIMARY KEY (contentid)


)