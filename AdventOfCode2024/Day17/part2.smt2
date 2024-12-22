(set-logic QF_BV)

(define-fun f ((x (_ BitVec 64))) (_ BitVec 64)
    (bvxor
        (bvxor (bvand x (_ bv7 64)) (_ bv4 64))
        (bvand (bvlshr x (bvxor (bvand x (_ bv7 64)) (_ bv1 64)))
               (_ bv7 64))))

(declare-const a (_ BitVec 64))
(assert (bvugt a (_ bv0 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv0 64)))) (_ bv2 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv1 64)))) (_ bv4 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv2 64)))) (_ bv1 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv3 64)))) (_ bv1 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv4 64)))) (_ bv7 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv5 64)))) (_ bv5 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv6 64)))) (_ bv1 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv7 64)))) (_ bv5 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv8 64)))) (_ bv4 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv9 64)))) (_ bv5 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv10 64)))) (_ bv0 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv11 64)))) (_ bv3 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv12 64)))) (_ bv5 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv13 64)))) (_ bv5 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv14 64)))) (_ bv3 64)))
(assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv15 64)))) (_ bv0 64)))
(minimize a)
(check-sat)
(get-value (a))
