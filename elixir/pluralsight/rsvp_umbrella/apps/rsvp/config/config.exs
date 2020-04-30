import Config

config :rsvp, Rsvp.Repo,
    adapter: Ecto.Adapters.Postgres,
    database: "rsvp",
    username: "postgres",
    password: "postgres"

config :rsvp, ecto_repos: [Rsvp.Repo]