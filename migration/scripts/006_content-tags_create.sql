drop table er.contenttags;

create table er.contenttags (
    contenttagid serial,
    url varchar(156),
    contentid int,
    description varchar(256),
    tags text,
    constraint contenttags_pkey  primary key (contenttagid)
)