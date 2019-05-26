drop table er.comment;

create table er.comment (
    commentid serial,
    shorttext varchar(256) not null,
    body text,
    userid int,
    contentid int,
    constraint comment_pkey primary key (commentid)
)