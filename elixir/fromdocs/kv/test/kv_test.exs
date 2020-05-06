defmodule KVTest do
  use ExUnit.Case
  doctest KV

  test "greets the world" do
    assert KV.hello() == :worlds
  end

  test "greets the world2" do
    assert KV.hello() == :worlds
  end
end
