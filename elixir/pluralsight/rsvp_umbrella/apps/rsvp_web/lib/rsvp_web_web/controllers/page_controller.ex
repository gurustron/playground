defmodule RsvpWebWeb.PageController do
  use RsvpWebWeb, :controller

  def index(conn, _params) do
    render(conn, "index.html")
  end

end
