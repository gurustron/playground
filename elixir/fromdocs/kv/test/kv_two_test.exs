defmodule KVTwoTest do
  use ExUnit.Case, async: true
  use ExVCR.Mock, adapter: ExVCR.Adapter.Hackney

  setup_all do
    HTTPoison.start
  end
  
  test "get request1" do
    use_cassette "httpoison_get2" do
        IO.puts("st 2_1")
        assert HTTPoison.get!("http://slowwly.robertomurray.co.uk/delay/1000/url/https://www.google.com").status_code == 302
       :timer.sleep(200)
       IO.puts("end 2_1")
      end
  end

  test "get request2" do
    use_cassette "httpoison_get2" do
        IO.puts("st 2_2")
        assert  HTTPoison.get!("http://slowwly.robertomurray.co.uk/delay/1000/url/https://www.google.com").status_code == 302
       :timer.sleep(200)
       IO.puts("end 2_2")
      end
  end
  test "get request3" do
    use_cassette "httpoison_get2" do
        IO.puts("st 2_3")
        assert  HTTPoison.get!("http://slowwly.robertomurray.co.uk/delay/1000/url/https://www.google.com").status_code == 302
       :timer.sleep(200)
       IO.puts("end 2_3")
      end
  end
  test "get request4" do
    use_cassette "httpoison_get2" do
        IO.puts("st 2_4")
        assert  HTTPoison.get!("http://slowwly.robertomurray.co.uk/delay/1000/url/https://www.google.com").status_code == 302
       :timer.sleep(200)
       IO.puts("end 2_4")
      end
  end
  test "get request5" do
    use_cassette "httpoison_get2" do
        IO.puts("st 2_5")
        assert  HTTPoison.get!("http://slowwly.robertomurray.co.uk/delay/1000/url/https://www.google.com").status_code == 302
       :timer.sleep(200)
       IO.puts("end 2_5")
      end
  end
end
