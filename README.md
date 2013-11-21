Configure with

<appender name="RedisAppender" type="Lokki.Utils.RedisAppender.RedisAppender">
	<listName value="name:of:my:redis:list" />
	<trimList value="true" />
	<maxLength value="1000" />
	<redisHost value="localhost:6379" />
</appender>