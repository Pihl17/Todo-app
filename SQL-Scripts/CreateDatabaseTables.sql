-- Set up enum types
CREATE TYPE STATUS AS ENUM('not_done', 'in_progress', 'done');
CREATE TYPE REPEAT_INTERVAL AS ENUM('none', 'daily', 'weekly', 'weekdays', 'monthly', 'yearly');

-- Set up tables on the database
CREATE TABLE todos (
	todo_id UUID NOT NULL PRIMARY KEY,
	title VARCHAR(255) NOT NULL,
	description VARCHAR(255) NOT NULL,
	status STATUS NOT NULL,
	priority INT NOT NULL,
	created_date DATE,
	finished_date DATE,
	deadline_date DATE,
	repeat_interval REPEAT_INTERVAL NOT NULL
);

CREATE TABLE tags (
	attached_todo_id UUID,
	tag VARCHAR(255)
);

CREATE TABLE checkitems (
	attached_todo_id UUID,
	description VARCHAR(255),
	is_done BOOL 
);

SELECT * FROM todos;