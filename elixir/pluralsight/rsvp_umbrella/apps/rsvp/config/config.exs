import Config

config :rsvp, ecto_repos: [Rsvp.Repo]

config :rsvp, Rsvp.Repo,
    adapter: Ecto.Adapters.Postgres,
    database: "rsvp",
    username: "postgres",
    password: "postgres"

