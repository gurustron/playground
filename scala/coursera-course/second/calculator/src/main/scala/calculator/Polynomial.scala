package calculator

object Polynomial extends PolynomialInterface {
  def computeDelta(a: Signal[Double], b: Signal[Double],
      c: Signal[Double]): Signal[Double] = {
    Var(math.pow(b(), 2) - 4 * a() * c())
  }

  def computeSolutions(a: Signal[Double], b: Signal[Double],
      c: Signal[Double], delta: Signal[Double]): Signal[Set[Double]] = {
    Var(
      if(delta() < 0) Set() else
      Set(
      (-b() + math.pow(delta(), 0.5)) / (2 * a()),
      (-b() - math.pow(delta(), 0.5)) / (2 * a())
    ))
  }
}
