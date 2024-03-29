// SpriteBreakerUtil is a C# port of the polybooljs library
// polybooljs is (c) Copyright 2016, Sean Connelly (@voidqk), http://syntheti.cc
// MIT License

namespace SpriteBreakerUtil {
    using System;

    public class PolyBool {

        #region Core api

        public SegmentList segments( Polygon poly, float scale=1.0f ) {
            var i = new Intersecter( true );

            foreach ( var region in poly.regions ) {
                i.addRegion( region, scale );
            }

            var result = i.calculate( poly.inverted );
            result.inverted = poly.inverted;

            return result;
        }

        public CombinedSegmentLists combine( SegmentList segments1, SegmentList segments2 ) {
            var i = new Intersecter( false );

            return new CombinedSegmentLists() {
                combined = i.calculate(
                    segments1, segments1.inverted,
                    segments2, segments2.inverted
                ),
                inverted1 = segments1.inverted,
                inverted2 = segments2.inverted
            };
        }

        public SegmentList selectUnion( CombinedSegmentLists combined ) {
            var result = SegmentSelector.union( combined.combined );
            result.inverted = combined.inverted1 || combined.inverted2;

            return result;
        }

        public SegmentList selectIntersect( CombinedSegmentLists combined ) {
            var result = SegmentSelector.intersect( combined.combined );
            result.inverted = combined.inverted1 && combined.inverted2;

            return result;
        }

        public SegmentList selectDifference( CombinedSegmentLists combined ) {
            var result = SegmentSelector.difference( combined.combined );
            result.inverted = combined.inverted1 && !combined.inverted2;

            return result;
        }

        public SegmentList selectDifferenceRev( CombinedSegmentLists combined ) {
            var result = SegmentSelector.differenceRev( combined.combined );
            result.inverted = !combined.inverted1 && combined.inverted2;

            return result;
        }

        public SegmentList selectXor( CombinedSegmentLists combined ) {
            var result = SegmentSelector.xor( combined.combined );
            result.inverted = combined.inverted1 != combined.inverted2;

            return result;
        }

        public Polygon polygon( SegmentList segments, float scale=1.0f ) {
            var chain = new SegmentChainer().chain( segments, scale );

            return new Polygon() {
                regions = chain,
                inverted = segments.inverted
            };
        }

        #endregion

        #region Helper functions for common operations

        public Polygon union( Polygon poly1, Polygon poly2 ) {
            return operate( poly1, poly2, selectUnion );
        }

        public Polygon intersect( Polygon poly1, Polygon poly2 ) {
            return operate( poly1, poly2, selectIntersect );
        }

        public Polygon difference( Polygon poly1, Polygon poly2 ) {
            return operate( poly1, poly2, selectDifference );
        }

        public Polygon differenceRev( Polygon poly1, Polygon poly2 ) {
            return operate( poly1, poly2, selectDifferenceRev );
        }

        public Polygon xor( Polygon poly1, Polygon poly2 ) {
            return operate( poly1, poly2, selectXor );
        }

        #endregion

        #region Private utility functions

        private Polygon operate( Polygon poly1, Polygon poly2, Func<CombinedSegmentLists, SegmentList> selector ) {
            var seg1 = segments( poly1 );
            var seg2 = segments( poly2 );
            var comb = combine( seg1, seg2 );

            var seg3 = selector( comb );

            return polygon( seg3 );
        }

        #endregion

        public static readonly PolyBool instance = new PolyBool();
    }

}
