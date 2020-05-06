defmodule Rsvp.Repo do
    use Ecto.Repo, otp_app: :rsvp, adapter: Ecto.Adapters.Postgres

    def test() do
        :ok
    end
end