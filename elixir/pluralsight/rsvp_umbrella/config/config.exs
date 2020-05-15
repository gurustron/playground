# This file is responsible for configuring your umbrella
# and **all applications** and their dependencies with the
# help of the Config module.
#
# Note that all applications in your umbrella share the
# same configuration and dependencies, which is why they
# all use the same configuration file. If you want different
# configurations or dependencies per app, it is best to
# move said applications out of the umbrella.
import Config

# Sample configuration:
#
#     config :logger, :console,
#       level: :info,
#       format: "$date $time [$level] $metadata$message\n",
#       metadata: [:user_id]
#

# Configures the endpoint
config :rsvp_web, RsvpWebWeb.Endpoint,
  url: [host: "localhost", port: 4444],
  secret_key_base: "t8INzjOwAwm+3tg3xcVTtXhNXZs9Ok+5EH/CkUadT3T7BZmwzQyfSbA0BgthDvbk",
  render_errors: [view: RsvpWebWeb.ErrorView, accepts: ~w(html json), layout: false],
  pubsub_server: RsvpWeb.PubSub,
  live_view: [signing_salt: "WHgbELuA"]

# Configures Elixir's Logger
config :logger, :console,
  format: "$time $metadata[$level] $message\n",
  metadata: [:request_id]

# Use Jason for JSON parsing in Phoenix
config :phoenix, :json_library, Jason

config :rsvp, Rsvp.Repo,
    adapter: Ecto.Adapters.Postgres,
    database: "rsvp",
    username: "postgres",
    password: "postgres"

config :rsvp, ecto_repos: [Rsvp.Repo]

# Import environment specific config. This must remain at the bottom
# of this file so it overrides the configuration defined above.
import_config "#{Mix.env()}.exs"