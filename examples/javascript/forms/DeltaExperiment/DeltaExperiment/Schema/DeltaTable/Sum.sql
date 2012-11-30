select sum(x), sum(y), sum(z)
 from Delta
 where ticks >= @ticks
