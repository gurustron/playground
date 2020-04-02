package calculator

sealed abstract class Expr
final case class Literal(v: Double) extends Expr
final case class Ref(name: String) extends Expr
final case class Plus(a: Expr, b: Expr) extends Expr
final case class Minus(a: Expr, b: Expr) extends Expr
final case class Times(a: Expr, b: Expr) extends Expr
final case class Divide(a: Expr, b: Expr) extends Expr

object Calculator extends CalculatorInterface {
  def computeValues(
      namedExpressions: Map[String, Signal[Expr]]): Map[String, Signal[Double]] = {
    namedExpressions.map{ case (str, value) => (str, Var(eval(value(), namedExpressions)))}
  }

  def eval(expr: Expr, references: Map[String, Signal[Expr]]): Double = {
    def inner(expr: Expr, references: Map[String, Signal[Expr]], names: Set[String]): Double = {
      expr match {
        case Literal(v) => v
        case Ref(name) =>
          if (names.contains(name))
            Double.NaN
          else
            inner(getReferenceExpr(name, references), references, names + name)
        case Plus(a, b) => inner(a, references, names) + inner(b, references, names)
        case Minus(a, b) => inner(a, references, names) - inner(b, references, names)
        case Times(a, b) => inner(a, references, names) * inner(b, references, names)
        case Divide(a, b) => inner(a, references, names) / inner(b, references, names)
      }
    }

    inner(expr, references, Set())
  }

  /** Get the Expr for a referenced variables.
   *  If the variable is not known, returns a literal NaN.
   */
  private def getReferenceExpr(name: String,
      references: Map[String, Signal[Expr]]) = {
    references.get(name).fold[Expr] {
      Literal(Double.NaN)
    } { exprSignal =>
      exprSignal()
    }
  }
}
