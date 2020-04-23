use Mix.Config

config :pluralsight_tweet, PluralsightTweet.Scheduler, jobs: [
    {"* * * *", fn ->
        PluralsightTweet.FileReader.get_tweet_strings(
            Path.join("#{:code.priv_dir(:pluralsight_tweet)}", "sample.txt")
        )
        |> PluralsightTweet.TweetServer.tweet
    end }
]
