defmodule FileReaderTest do
    use ExUnit.Case
    import PluralsightTweet.FileReader
    import Mock

    test "passing a file returns string" do
        # str = PluralsightTweet.FileReader.get_tweet_strings(
        str = get_tweet_strings(
            Path.join("#{:code.priv_dir(:pluralsight_tweet)}", "sample.txt")
        )

        assert str != nil
    end

    test "pick_string nil -> nil" do
        str = pick_string("")
        assert  str == ""
    end

    test "mocking" do
        str = with_mock File, [read!: fn(_) -> " asdsad " end] do
            get_tweet_strings(Path.join("#{:code.priv_dir(:pluralsight_tweet)}", "sample.txt"))
        end

        assert str != nil
    end
end
