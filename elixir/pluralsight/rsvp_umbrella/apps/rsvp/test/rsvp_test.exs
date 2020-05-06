defmodule RsvpTest do
  use ExUnit.Case
  doctest Rsvp

  test "greets the world" do
    assert Rsvp.hello() == :world
  end
end
